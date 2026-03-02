import type { User } from '~/types/user'

export default defineEventHandler(async () => {
  const config = useRuntimeConfig()

  return await $fetch<User[]>('/api/users', {
    baseURL: config.baseApi
  })
})
