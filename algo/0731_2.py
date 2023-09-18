#  전광판
num = [[1,2,3,5,6,7],[6,7],[2,3,4,5,6],[3,4,5,6,7],[1,3,4,5,7],[1,2,4,5,7],[1,3,6,7],[1,2,3,4,5,6,7],[1,3,4,5,6,7]]
for _ in range(int(input())):
    ret = 0
    s, e = [[],[],[],[],[]], [[],[],[],[],[]]
    sn, en = map(int, input().split())

    sn = str(sn)
    en = str(en)
    sl = len(sn)
    el = len(en)

    n = 4
    for i in range(sl):
        s[n-i] = num[int(sn[i])]

    for i in range(el):
        e[n-i] = num[int(en[i])]

    for i in range(5):
        for j in range(len(s[i])):
            if s[i][j] not in e[i]:
                ret += 1

    print(ret)
