package main

import (
	"fmt"
	"math/rand"
	"sync"
	"time"
)

type Fetcher interface {
	// Fetch returns the body of URL and
	// a slice of URLs found on that page.
	Fetch(url string) (body string, urls []string, err error)
}

type safeMap struct {
	mutex   sync.Mutex
	visited map[string]bool
}

var storage = safeMap{
	visited: make(map[string]bool),
}

// Crawl uses fetcher to recursively crawl
// pages starting with url, to a maximum of depth.
func Crawl(url string, depth int, fetcher Fetcher, chout chan string) {
	defer close(chout)

	if depth <= 0 {
		return
	}

	storage.mutex.Lock()
	if _, visited := storage.visited[url]; visited {
		storage.mutex.Unlock()
		return
	}

	storage.visited[url] = true
	storage.mutex.Unlock()

	body, urls, err := fetcher.Fetch(url)
	if err != nil {
		fmt.Println(err)
		return
	}

	chout <- fmt.Sprintf("found: %s %q\n", url, body)

	result := make([]chan string, len(urls))
	for i, u := range urls {
		result[i] = make(chan string)
		go Crawl(u, depth-1, fetcher, result[i])
	}

	for i := range result {
		for x := range result[i] {
			chout <- x
		}
	}

	return
}

func main() {
	chout := make(chan string)

	go Crawl("https://golang.org/", 4, fetcher, chout)

	for val := range chout {
		fmt.Print(val)
	}
}

// fakeFetcher is Fetcher that returns canned results.
type fakeFetcher map[string]*fakeResult

type fakeResult struct {
	body string
	urls []string
}

func (f fakeFetcher) Fetch(url string) (string, []string, error) {
	time.Sleep(time.Duration(rand.Int31n(100)) * time.Microsecond)

	if res, ok := f[url]; ok {
		return res.body, res.urls, nil
	}
	return "", nil, fmt.Errorf("not found: %s", url)
}

// fetcher is a populated fakeFetcher.
var fetcher = fakeFetcher{
	"https://golang.org/": &fakeResult{
		"The Go Programming Language",
		[]string{
			"https://golang.org/pkg/",
			"https://golang.org/cmd/",
		},
	},
	"https://golang.org/pkg/": &fakeResult{
		"Packages",
		[]string{
			"https://golang.org/",
			"https://golang.org/cmd/",
			"https://golang.org/pkg/fmt/",
			"https://golang.org/pkg/os/",
		},
	},
	"https://golang.org/pkg/fmt/": &fakeResult{
		"Package fmt",
		[]string{
			"https://golang.org/",
			"https://golang.org/pkg/",
		},
	},
	"https://golang.org/pkg/os/": &fakeResult{
		"Package os",
		[]string{
			"https://golang.org/",
			"https://golang.org/pkg/",
		},
	},
}
