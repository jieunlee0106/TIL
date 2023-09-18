def solution(n, m, records):
    disk_lst = [[] for _ in range(n)]
    ans = []

    for r in records:
        disk, data, date = r
        disk_lst[disk - 1].append([data, date])

    for disk in disk_lst:
        disk.sort(key=lambda x: (x[1], -x[0]))

    while True:
        disk_1st = [disk.pop(0) for disk in disk_lst if disk]

        if not disk_1st:
            break

        for i, (data, date) in enumerate(disk_1st):
            ans.append((i + 1, date))
            if len(disk_lst[i]) > 0:
                disk_1st[i] = disk_lst[i].pop(0)

        disk_1st.sort(key=lambda x: (x[1], -x[0]))
        disk_lst = [disk for disk in disk_lst if disk]
    print(ans)
    return ans

n= 2
m=3
records = [[1,2,7],[1,1,7],[1,3,9],[2,1,3],[2,2,9],[2,3,1]]

solution(n, m, records)