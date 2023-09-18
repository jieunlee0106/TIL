# matrix = [input() for _ in range(5)]
# delta = [(-1, 0), (1, 0), (0, -1), (0, 1)]
# result_set = set()
#
#
# def backtrack(arr, index=0, S=0, Y=0):
#     tmp = arr
#     if Y > 3:
#         return
#     if index == 6:
#         arr.sort()
#         result_set.add(tuple(arr))
#     else:
#         adjacents = []
#         for node in range(len(arr)):
#             for i in range(4):
#                 dx = arr[node][0] + delta[i][0]
#                 dy = arr[node][1] + delta[i][1]
#                 if 0 > dx or 5 <= dx or 0 > dy or 5 <= dy: continue
#                 if (dx, dy) in arr: continue
#                 adjacents.append((dx,dy))
#         for adjacent in adjacents:
#             nx = adjacent[0]
#             ny = adjacent[1]
#             if matrix[nx][ny] == 'S':
#                 backtrack(arr+[(nx,ny)], index+1, S+1, Y)
#             else:
#                 backtrack(arr+[(nx,ny)], index+1, S, Y+1)
#
# for i in range(5):
#     for j in range(5):
#         if matrix[i][j] == 'S':
#             backtrack([(i, j)], index=0, S=1)
#             #
#
# print(len(result_set))

from collections import deque
delta = [(0, 1),(0, -1), (1, 0), (-1, 0)]
def make_crew(cnt, sel, S_cnt):
    global ans
    # X 자리밖에 안남았는데 X 합해도 S_cnt 4를 못넘을떄
    if S_cnt + (7 - sel) < 4 : return
    if sel == 7 :
        if S_cnt >= 4 and check_bfs():
            ans += 1
        return
    if cnt == 25: return
    r = cnt // 5
    c = cnt % 5
    select[sel] = (r, c)
    make_crew(cnt + 1, sel + 1, S_cnt + 1 if arr[r][c] == 'S' else S_cnt)
    make_crew(cnt + 1, sel, S_cnt)
    return


def check_bfs():
    visited = [[0]*5 for _ in range(5)]
    check = 1

    for i in range(1, 7):
        visited[select[i][0]][select[i][1]] = 1

    Q = deque()
    Q.append((select[0][0], select[0][1]))

    while Q:
        r, c = Q.popleft()
        for dt in range(4):
            nr = r + delta[dt][0]
            nc = c + delta[dt][1]
            if nr < 0 or nc < 0 or nr >= 5 or nc >= 5 :continue
            if visited[nr][nc]:
                check += 1
                visited[nr][nc] = 0
                Q.append((nr, nc))
                if check == 7:
                    return True
    return False

arr = [input() for _ in range(5)]
select = [-1] * 7
ans = 0
make_crew(0, 0, 0)
print(ans)