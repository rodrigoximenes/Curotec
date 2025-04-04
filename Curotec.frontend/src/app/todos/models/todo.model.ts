export interface Todo {
    id: string;
    title: string;
    description: string;
    status: TaskStatusEnum;
    creationDate: Date;
    completionDate?: Date;
    assignee: string;
    priority: TaskPriorityEnum;
}

export enum TaskStatusEnum {
    Pending = 0,
    InProgress = 1,
    Completed = 2,
    Canceled = 3,
}

export enum TaskPriorityEnum {
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3,
}

