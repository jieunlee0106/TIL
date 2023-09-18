# Lv1. 근무시간
m = 0
s = 0
res = 0
for _ in range(5):
    s, e = map(str, input().split())

    sh = int(s[:2])*60
    sm = int(s[3:])
    s = sh + sm

    eh = int(e[:2])*60
    em = int(e[3:])
    e = eh + em

    res += e-s

print(res)
