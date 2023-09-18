# w, n = map(int, input().split())
# j = {}
# for _ in range(n):
#     g, m = map(int, input().split())
#     if m in j:
#         j[m] += g
#     else:
#         j[m] = g
# res = 0
# aa = sorted(j, reverse=True)
# for k in aa:
#     m = j[k]
#     if w == 0:
#         print(res)
#         break
#     else:
#         if w >= m:
#            w -= m
#            res += m*k
#         else:
#             res += w *k
#             print(res)
#             w = 0
#
# =>
import sys

input = sys.stdin.readline

w, n = map(int, input().split())

jewels = [list(map(int, input().split())) for _ in range(n)]

jewels.sort(key=lambda x: x[1], reverse=True)

answer = 0
for weight, price in jewels:
    if w > weight:
        answer += weight * price
        w -= weight
    else:
        answer += w * price
        break

print(answer)