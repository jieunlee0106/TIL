import sys

w, n = map(int, input().split())
dic = dict()
ans = 0

for _ in range(n):
  m, p = map(int, input().split())
  if p in dic:
    dic[p] += m 
  else:
    dic[p] = m

dic = dict(sorted(dic.items(), key=lambda x: x[0], reverse=True))
for k, v in dic.items():
    if w > 0:
      if w >= v:
        ans += k*v
        w -= v
      else:
        ans += k*w
        w = 0
    else:
      break
print(ans)
