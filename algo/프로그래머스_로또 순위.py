def solution(lottos, win_nums):
    answer = []
    lottos = list(set(lottos))
    if 0 in lottos:
        lottos.remove(0)
    ln = 6 - len(lottos)
    cnt = 0
    for i in range(len(lottos)):
        if lottos[i] in win_nums:
            cnt += 1
    if 7 - cnt >= 6:
        answer.append(6)
    else:
        answer.append(7-cnt)
    if 7 - cnt - ln >= 6:
        answer.append(6)
    if 7 - cnt - ln <6:
        answer.append(7-cnt-ln)
    return sorted(answer)
