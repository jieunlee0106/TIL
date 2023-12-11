
def find_minimum_boxes(n):
    # box = 상자 개수를 저장할 변수
    # 1. 5개짜리 상자로 최대한 많이 담는다
    box = n // 5
    n %= 5

    while box >= 0:
        # 2. 남은 옷이 3의 배수라면 3개짜리 상자에 담고 끝
        if n % 3 == 0:
            # 2-1. 5개 짜리 담긴 상자 개수 + 3개짜리 상자 개수
            return box + (n // 3)
        # 3. 5개짜리 상자를 하나 줄이고, 남은 옷에 5를 더하기
        box -= 1
        n += 5

    # 모든 옷을 담을 수 없으면 -1을 반환
    return -1

# 입력
n = int(input()) # 옷의 개수
# 출력
print(find_minimum_boxes(n))
