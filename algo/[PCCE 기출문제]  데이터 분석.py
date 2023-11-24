# def solution(data, ext, val_ext, sort_by):
#     answer = []
#     dic = {"code":0, "date": 1, "maximum:":2, "remain":3}
#     k = dic[sort_by]
#     for d in data:
#         if d[dic[ext]] < val_ext:
#             answer.append(d)
#     answer = sorted(answer, key = lambda x: x[k])
#     return answer

def solution(data, ext, val_ext, sort_by):     
    dic = {"code":0, "date": 1, "maximum:":2, "remain":3}     
    return sorted(list(filter(lambda x : x[dic[ext]] < val_ext, data)), key=lambda x:x[dic[sort_by]])
