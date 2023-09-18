#gbc
N, M = map(int, input().split())
lst = [0] * 101
rlst = [0] * 101
s = 0
rs = 0
for _ in range(N):
    m, ms = map(int, input().split())
    for i in range(s, s + m):
        lst[i] = ms
    s = s + m
for _ in range(M):
    m, ms = map(int, input().split())
    for i in range(rs, rs + m):
        rlst[i] = ms
    rs = rs + m

ret = 0
for i in range(101):
    if ret < (rlst[i] - lst[i]):
        ret = rlst[i] - lst[i]
print(ret)
