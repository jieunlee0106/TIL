import copy
def solution(bandage, health, attacks):
    answer = 0
    t, x, y = bandage[0], bandage[1], bandage[2]
    max_time = copy.deepcopy(health)
    s, e = 0, 0
    
    for i in attacks:
        a_t, power = i[0], i[1]
        e = a_t-1
        time = e - s
        health = health + time*x + (time//t)*y
        if health > max_time:
            health = copy.deepcopy(max_time)
        health -= power
        s = a_t
        if 0 >= health:
            return -1
    return health
