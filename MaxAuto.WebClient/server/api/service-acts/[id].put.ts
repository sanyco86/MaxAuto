import type { EventHandler } from 'h3'
import { z } from 'zod'

const BodySchema = z.object({
  name: z.string().min(2).max(200),
})

const handler: EventHandler = async (event) => {
  const config = useRuntimeConfig()
  const { id } = getRouterParams(event)

  if (!id) {
    throw createError({ statusCode: 400, statusMessage: 'id is required' })
  }

  const body = await readBody(event)
  const parsed = BodySchema.safeParse(body)
  if (!parsed.success) {
    throw createError({
      statusCode: 422,
      statusMessage: parsed.error.issues.map(i => i.message).join(', ')
    })
  }

  return await $fetch(`/api/service-acts/${id}`, {
    baseURL: config.baseApi,
    method: 'PUT',
    body: parsed.data
  })
}

export default handler
