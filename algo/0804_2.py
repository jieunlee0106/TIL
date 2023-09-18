# 바이러스 _ 오버플로우 조심
virus, rate, n = map(int, input().split())

for i in range(n):
    virus = virus * rate
    virus = virus % 1000000007


print(virus%1000000007)