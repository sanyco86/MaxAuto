import type { Workshop } from "~/types/workshop";

export type UserRoles = 'admin' | 'owner' | 'manager' | 'visitor'

export interface User {
  id: number
  firstName: string
  lastName: string
  email: string
  role: UserRoles
  position?: string
  workshop?: Workshop
}
