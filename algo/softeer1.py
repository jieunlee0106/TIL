import sys

n = int(input())  # n = 참가자의 명수

arr = [list(map(int, input().split())) for _ in range(3)]
# 이차원 배열로 저장 arr[0]은 첫번째 대회의 각 참가자의 점수를 의미한다

total_score = [0] * n

for i in range(len(arr)):
    result = []  # 각 대회의 등수 결과를 저장할 리스트

    for a in range(n):
        rank = 1
        total_score[a] += arr[i][a]  # 전체 등수를 구하기 위해 각 참가자별 점수를 누적계산해서 저장한다
        for b in range(n):
            if arr[i][a] < arr[i][b]:  # 모든 요소와 비교하여 작을때만 랭크를 1 더한다
                rank += 1
        result.append(rank)
    print(*result)  # 현재 대회의 각 참가자의 등수를 출력한다

total_rank = []
for a in range(n):
    rank = 1
    for b in range(n):
        if total_score[a] < total_score[b]:
            rank += 1
    total_rank.append(rank)
print(*total_rank)