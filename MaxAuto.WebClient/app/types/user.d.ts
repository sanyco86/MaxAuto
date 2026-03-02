import type { Workshop } from "~/types/workshop";

export type UserRoles = 'admin' | 'owner' | 'manager' | 'visitor'

export interface User {
  id: string
  firstName: string
  lastName: string
  email: string
  role: UserRoles
  position?: string
  workshop?: Workshop
}
