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
    Pending = 'Pending',
    InProgress = 'InProgress',
    Completed = 'Completed',
    Canceled = 'Canceled',
}

export enum TaskPriorityEnum {
    Low = 'Low',
    Medium = 'Medium',
    High = 'High',
    Critical = 'Critical',
}
