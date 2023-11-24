def solution(board, h, w):
    answer = 0
    rl, cl  = len(board), len(board[0])
    for i in [[-1,0], [1,0], [0,-1], [0,1]]:
        if 0 <= h + i[0] < rl and 0 <= w + i[1] < cl and board[h + i[0]][w + i[1]] == board[h][w]:
            answer += 1               
    return answer
