p = [ input() for _ in range(5)]
dt = [[-1, 0], [1, 0], [0, -1], [0, 1]]
ret = set()
def b(arr, S=0, Y=0, cnt=0):
    tem = arr
    if Y > 3:
        return
    if cnt == 6:
        arr.sort()
        ret.add(tuple(arr))

    else:
        ad = []
        for n in range(len(arr)):
            for d in range(4):
                R = arr[n][0] + dt[d][0]
                C = arr[n][1] + dt[d][1]
                if R >=5 or R < 0 or C >= 5 or C < 0: continue
                if (R, C) in arr: continue
                ad.append((R, C))
        for a in ad:
            rr = a[0]
            cc = a[1]
            if p[rr][cc] == "S":
                b(arr + [(rr, cc)], S+1, cnt+1, Y)
            else:
                b(arr + [(rr, cc)], S, cnt+1, Y+1)

for i in range(5):
    for j in range(5):
        if p[i][j] == "S":
            b([(i, j)], cnt=0,  S=1)
print(ret)
print(len(ret))