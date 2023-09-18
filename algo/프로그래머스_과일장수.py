def solution(k, m, score):
    ans = 0
    score.sort(reverse=True)
    n = len(score)//m
    lst = []
    s = 0
    for i in range(n):
        lst = score[s:s+m]
        if min(lst) >= k:
            ans += k*m
        else:
            ans += m*min(lst)

        s += m
        lst = []
    return ans