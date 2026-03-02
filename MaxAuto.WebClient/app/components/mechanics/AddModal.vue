<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import type { Mechanic } from '~/types/mechanic'

const emit = defineEmits<{
  (e: 'created', mechanic: Mechanic): void
}>()

const schema = z.object({
  name: z.string().min(2, 'Слишком коротко'),
})

type Schema = z.output<typeof schema>

const open = ref(false)
const state = reactive<Partial<Schema>>({ name: '' })
const toast = useToast()

async function onSubmit(event: FormSubmitEvent<Schema>) {
  const mechanic = await $fetch<Mechanic>('/api/mechanics', {
    method: 'POST',
    body: { name: event.data.name },
  })

  toast.add({
    title: 'Успех',
    description: `Новый механик ${mechanic.name} успешно добавлен`,
    color: 'success',
  })

  open.value = false
  state.name = ''

  emit('created', mechanic)
}
</script>

<template>
  <UModal v-model:open="open" title="Механик" description="Добавить нового механика">
    <UButton label="Добавить нового механика" icon="i-lucide-plus" />

    <template #body>
      <UForm :schema="schema" :state="state" class="space-y-4" @submit="onSubmit">
        <UFormField label="Имя" name="name">
          <UInput v-model="state.name" class="w-full" />
        </UFormField>
        <div class="flex justify-end gap-2">
          <UButton label="Отмена" color="neutral" variant="subtle" @click="open = false" />
          <UButton label="Создать" color="primary" variant="solid" type="submit" />
        </div>
      </UForm>
    </template>
  </UModal>
</template>
