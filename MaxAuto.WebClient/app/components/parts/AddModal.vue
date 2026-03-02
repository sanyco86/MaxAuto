<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import type { Part } from '~/types/part'

const emit = defineEmits<{
  (e: 'created', part: Part): void
}>()

const schema = z.object({
  name: z.string().min(2, 'Слишком коротко'),
})

type Schema = z.output<typeof schema>

const open = ref(false)
const state = reactive<Partial<Schema>>({ name: '' })
const toast = useToast()

async function onSubmit(event: FormSubmitEvent<Schema>) {
  const part = await $fetch<Part>('/api/parts', {
    method: 'POST',
    body: { name: event.data.name },
  })

  toast.add({
    title: 'Успех',
    description: `Новая запчасть ${part.name} успешно создана`,
    color: 'success',
  })

  open.value = false
  state.name = ''

  emit('created', part)
}
</script>

<template>
  <UModal v-model:open="open" title="Запчасть" description="Добавить новую запчасть">
    <UButton label="Добавить новую запчасть" icon="i-lucide-plus" />

    <template #body>
      <UForm :schema="schema" :state="state" class="space-y-4" @submit="onSubmit">
        <UFormField label="Название" name="name">
          <UInput v-model="state.name" class="w-full" />
        </UFormField>
        <div class="flex justify-end gap-2">
          <div class="flex justify-end gap-2">
            <UButton label="Отмена" color="neutral" variant="subtle" @click="open = false" />
            <UButton label="Создать" color="primary" variant="solid" type="submit" />
          </div>
        </div>
      </UForm>
    </template>
  </UModal>
</template>
