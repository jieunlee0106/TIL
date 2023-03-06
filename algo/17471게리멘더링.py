import sys
from itertools import combinations
from collections import deque

n = int(sys.stdin.readline().rstrip())
people = list(map(int, sys.stdin.readline().rstrip().split()))
arr = [[0 for _ in range(n)] for _ in range(n)]

for i in range(n):
    _, *dsts = map(int, sys.stdin.readline().rstrip().split())
    for dst in dsts:
        arr[i][dst - 1] = 1


def is_connected(nodes):
    q = deque()
    check = [False for _ in range(n)]
    q.append(nodes[0])
    check[nodes[0]] = True

    while q:
        node = q.popleft()

        for i in range(len(arr[node])):
            if arr[node][i] == 0: continue
            if i not in nodes: continue
            if not check[i]:
                check[i] = True
                q.append(i)

    return check.count(True) == len(nodes)


def get_total(nodes):
    total = 0
    for node in nodes:
        total += people[node]

    return total


cases = []
X = {i for i in range(n)}
INF = int(1e9)
ans = INF

for i in range(1, n // 2 + 1):
    As = tuple(combinations(X, i))
    for A in As:
        B = list(X.difference(A))

        if is_connected(A) and is_connected(B):
            a_total = get_total(A)
            b_total = get_total(B)
            ans = min(ans, abs(a_total - b_total))

if ans == INF:
    print(-1)
else:
    print(ans)