# 프로그래머스 Lv1. 달리기 경주
players = ["mumu", "soe", "poe", "kai", "mine"]
callings = ["kai", "kai", "mine", "mine"]


def solution(players, callings):
    player_dict = {player: rank for rank, player in enumerate(players)}
    rank_dict = {rank: player for rank, player in enumerate(players)}

    for call in callings:
        rank = player_dict[call]

        player_dict[rank_dict[rank - 1]], player_dict[rank_dict[rank]] = player_dict[rank_dict[rank]], player_dict[
            rank_dict[rank - 1]]
        rank_dict[rank - 1], rank_dict[rank] = rank_dict[rank], rank_dict[rank - 1]

    return list(rank_dict.values())
solution(players, callings)

# def solution(players, callings):
#     player_dict = {}
#
#     for i in range(len(players)):
#         player_dict[i] = players[i]
#
#     for c in callings:
#         for k, v in player_dict.items():
#             if v == c:
#                 if k > 0:
#                     player_dict[k], player_dict[k - 1] = player_dict[k - 1], player_dict[k]
#
#     answer = [player_dict[k] for k in sorted(player_dict.keys())]
#     return answer

# test 7~13 시간초과
# def solution(players, callings):
#     player_dict = {}
#
#     for i in range(len(players)):
#         player_dict[i] = players[i]
#         # {1: asd, 2 : fs}
#
#     for c in callings:
#         for k, v in player_dict.items():
#             if v == c:
#                 player_dict[k] = player_dict[k - 1]
#                 player_dict[k - 1] = v
#
#     answer = []
#     ans = sorted(player_dict.items())
#     for a in ans:
#         answer.append(a[1])
#     return answer