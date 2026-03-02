import type { Mechanic } from "~/types/mechanic";

export default defineEventHandler(async () => {
  const config = useRuntimeConfig()

  return await $fetch<Mechanic[]>('/api/mechanics', {
    baseURL: config.baseApi
  })
})
