import type { Unit } from "~/types/unit";

export default defineEventHandler(async () => {
  const config = useRuntimeConfig()

  return await $fetch<Unit[]>('/api/units', {
    baseURL: config.baseApi
  })
})
