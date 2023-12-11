from collections import deque
def booking_room(customer, k):
    q = deque() # 예약 대기자 고객 Id를 담는 Queue
    room = [] # 방을 배정 받은 고객 Id를 담는 List
    for c in customer:
        customer_id, is_booking = c[0], c[1]
        # 예약
        if is_booking == 1:
            if k > 0: # 남은 방 있을 경우
                room.append(customer_id)
                k -= 1
            else: # 남은 방 없을 경우
                q.append(customer_id)
        # 예약 취소
        if is_booking == 0:
            if customer_id in room: # 방을 배정 받은 경우
                room.remove(customer_id)
                k += 1
            if customer_id in q: # 대기자 명단에 있는 경우
                q.remove(customer_id)
        # 빈 방 있을 시 우선 대기자 부터 방 배정
        if k > 0 and q:
            room.append(q.popleft())
            k -= 1
    return room

# 입력
# ------ 1번
# customer = [[1,1],[2,1],[3,1],[2,0],[2,1]]
# k = 2
# 출력 결과 => [1,3]

# ------ 2번
# customer = [[2,1],[1,1],[3,1],[1,0],[1,1],[2,0],[2,1]]
# k = 1
# 출력 결과 => [3]

# 출력
print(booking_room(customer, k))
