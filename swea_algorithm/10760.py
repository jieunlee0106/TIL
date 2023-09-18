for t in range(int(input())):
    r, c = map(int, input().split())
    arr = [list(map(int, input().split())) for _ in range(r)]
    lst = [[0 for _ in range(c)] for _ in range(r)]
    dt = [[-1, -1], [-1, 0], [-1, 1], [0, -1], [0, 1], [1, -1], [1, 0], [1, 1]]
    for i in range(r):
        for j in range(c):
            for d in range(8):
                R = i + dt[d][0]
                C = j + dt[d][1]
                if 0 <= R < r and 0 <= C < c:
                    if arr[R][C] > arr[i][j]:
                        lst[R][C] += 1
    ret = 0
    for i in range(r):
        for j in range(c):
            if lst[i][j] >= 4:
                ret += 1

    print(f'#{t+1} {ret}')

