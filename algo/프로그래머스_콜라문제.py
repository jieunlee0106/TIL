def solution(a, b, n):
    ans = 0
    while n >= a:
        ans += b*(n//a)
        n = (n//a)*b + (n%a)        
    return ans