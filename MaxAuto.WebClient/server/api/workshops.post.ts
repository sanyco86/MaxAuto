import { z } from 'zod'
import type { Workshop } from "~/types/workshop";

const BodySchema = z.object({
  name: z.string().min(2).max(200),
  location: z.string().min(2).max(200),
  address: z.string().optional(),
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

  return await $fetch<Workshop>('/api/workshops', {
    baseURL: config.baseApi,
    method: 'POST',
    body: parsed.data
  })
})
