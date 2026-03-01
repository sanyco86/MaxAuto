export type ServiceActStatus = 'opened' | 'completed'

export interface ServiceAct {
  id: number
  name: string
  status: ServiceActStatus
}
