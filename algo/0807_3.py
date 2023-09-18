# 프로그래머스 Lv1. 기사단원의 무기

def solution(number, limit, power):
    lst = [0] * number
    for n in range(1, number + 1):
        for i in range(1, n + 1):
            if n % i == 0:
                lst[n - 1] += 1
    res = 0
    for i in lst:
        if i > limit:
            res += power
        else:
            res += i

    return res