import sys
sys.setrecursionlimit(1500)

def check_go(r, c):
    if checked[r][c] != 0:
        return
    checked[r][c] = 1
    for i in range(4):
        R = r + dr[i]
        C = c + dc[i]
        if 0 <= R < n and 0 <= C < n:
            if arr[R][C] != 0 and checked[R][C] == 0:
                arr[R][C] = cnt
                check_go(R, C)
    return

n = int(input())
checked = [[0] * n for _ in range(n)]


arr = []
for _ in range(n):
    arr.append(list(map(int, input())))

dr = [-1, 1, 0, 0]
dc = [0, 0, -1, 1]

cnt = 1

for r in range(n):
    for c in range(n):
        if arr[r][c] != 0 and checked[r][c] == 0:
            arr[r][c] = cnt
            check_go(r, c)
            cnt += 1

ans = 0
for r in range(n):
    for c in range(n):
        if arr[r][c] > ans:
            ans = arr[r][c]
print(ans)
lst = [0 for _ in range(ans + 1)]
for r in range(n):
    for c in range(n):
        if arr[r][c] != 0:
            lst[arr[r][c]] += 1
lst.sort(reverse=True)
lst.pop()
while lst:
    print(lst.pop())

# 7
# 1110111
# 0110101
# 0110101
# 0000100
# 0110000
# 0111110
# 0110000