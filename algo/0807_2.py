# 프로그래머스 Lv1. 나머지 1되는 수
def solution(n):
    x = n-1
    answer = 0
    for i in range(2, 1000001):
        if x%i == 0:
            answer = i
            break
    return answer