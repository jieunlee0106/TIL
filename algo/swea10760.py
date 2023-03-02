T = int(input())
dr = [-1, 1, 0, 0, -1, -1, 1, 1]
dc = [0, 0, -1, 1, -1, 1, -1, 1]
def go (r, c, s):

    global cnt
    ss = 0

    for d in range(8):
        R = r + dr[d]
        C = c + dc[d]

        if 0 <= R < n and 0 <= C < m:
            if arr[R][C] < s:
                ss += 1
                if ss >= 4:
                    return True

for t in range(T):
    n, m = map(int, input().split())
    arr = [list(map(int, input().split())) for _ in range(n)]
    cnt = 0
    for i in range(n):
        for j in range(m):
            spot = arr[i][j]
            ret = go(i, j, spot)
            if ret:
                cnt += 1
    print(f'#{t} {cnt}')