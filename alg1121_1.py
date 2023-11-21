import sys

n, k = map(int, input().split())
score = list(map(int, input().split()))

for _ in range(k):
  s, e = map(int, input().split())
  s -= 1
  e -= 1
  sum_num = 0
  for i in range(s, e+1):
    sum_num += score[i]
  rst = sum_num/(e-s+1)
  print("{:.2f}".format(round(rst, 3)))
