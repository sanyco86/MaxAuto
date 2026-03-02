import type { Workshop } from "~/types/workshop";

export default defineEventHandler(async () => {
  const config = useRuntimeConfig()

  return await $fetch<Workshop[]>('/api/workshops', {
    baseURL: config.baseApi
  })
})
