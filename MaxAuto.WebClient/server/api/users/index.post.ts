import { z } from 'zod'
import type { User } from '~/types/user'

const BodySchema = z.object({
  firstName: z.string().min(2).max(200),
  lastName: z.string().min(2).max(200),
  email: z.email(),
  password: z.string().min(6).optional(),
  position: z.string().optional(),
  role: z.enum(['admin', 'owner', 'manager', 'visitor']),
  workshopId: z.string().optional(),
})

export default defineEventHandler(async (event) => {
  const config = useRuntimeConfig()

  const body = await readBody(event)
  const parsed = BodySchema.safeParse(body)

  if (!parsed.success) {
    throw createError({
      statusCode: 422,
      statusMessage: parsed.error.issues.map(i => i.message).join(', ')
    })
  }

  return await $fetch<User>('/api/users', {
    baseURL: config.baseApi,
    method: 'POST',
    body: parsed.data
  })
})
