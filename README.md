# Task
Using Task for multitasking and monitor for locking.
Shared data: int[]storage, size: command line parameter
  - data values: all initially 0, when produced: 1..n where 1..n is producer id
  - number of tasks/threads: command line parameter.
Each producer stop when the storage is full (no 0 values anymore)
