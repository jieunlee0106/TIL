import sys

n, m = map(int, input().split())
arr = [[] for _ in range(n)]
cnt = 0

lst = list(map(int, input().split()))
for _ in range(m):
  p1, p2 = map(int, input().split())
  arr[p1-1].append(lst[p2-1])
  arr[p2-1].append(lst[p1-1])

for i in range(n):
  isok = True
  for j in arr[i]:
    if isok != True:
      break
    if lst[i] <= j:
      isok = False
  if isok != False:
    cnt += 1
print(cnt)
