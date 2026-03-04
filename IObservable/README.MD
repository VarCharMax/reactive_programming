\##Explanation



As I understand it, this example was supposed to show what happens when an observer goes out of scope without being managed correctly - it becomes a zombie - not reacting, but not terminating.



However, I can't get it to exhibit that exact behavior. It doesn't shut down, but it does keep reacting, and you don't see the END message as per their screenshot.



I tried running their supplied code, and got the same result. So I'm wondering if they really tested it.

