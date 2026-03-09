Intended to show the difference between an observer ending by going out-of-scope compared to being explicitly terminated.



It's a bit misleading, because the `Dispose()` method gets called in both cases. But in the former case, the `dispose()` callback alone is responsible for ending the observer, whereas in the latter, it essentially performs a clean-up after the object has been explicitly ended.

