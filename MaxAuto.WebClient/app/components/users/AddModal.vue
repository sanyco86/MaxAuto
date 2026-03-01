<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'


const schema = z.object({
  firstName: z.string().min(2, 'Слишком коротко'),
  lastName: z.string().min(2, 'Слишком коротко'),
  email: z.email('Невалидный email'),
  password: z.string().min(6, 'Должен быть не менее 6 символов').optional(),
  position: z.string().optional(),
  role: z.enum(['admin', 'owner', 'manager', 'visitor']),
  workshopId: z.string().optional(),
})

const open = ref(false)

type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({
  firstName: '',
  lastName: '',
  email: '',
  password: '',
  position: '',
  role: 'visitor',
  workshopId: ''
})

const toast = useToast()
async function onSubmit(event: FormSubmitEvent<Schema>) {
  toast.add({ title: 'Успех', description: `Пользователь ${event.data.email} успешно создан`, color: 'success' })
  open.value = false
}
</script>

<template>
  <UModal v-model:open="open" title="Пользователь" description="Создать нового пользователя">
    <UButton label="Добавить нового пользователя" icon="i-lucide-plus" />

    <template #body>
      <UForm
        :schema="schema"
        :state="state"
        class="space-y-4"
        @submit="onSubmit"
      >
        <UFormField label="Имя" name="firstName">
          <UInput v-model="state.firstName" class="w-full" />
        </UFormField>
        <UFormField label="Фамилия" name="lastName">
          <UInput v-model="state.lastName" class="w-full" />
        </UFormField>
        <UFormField label="Email" name="email">
          <UInput v-model="state.email" class="w-full" />
        </UFormField>
        <UFormField label="Пароль" name="password">
          <UInput v-model="state.password" class="w-full" />
        </UFormField>
        <UFormField label="Должность" name="position">
          <UInput v-model="state.position" class="w-full" />
        </UFormField>
        <UFormField label="Роль" name="role">
          <UInput v-model="state.role" class="w-full" />
        </UFormField>
        <UFormField label="Сервис" name="workshopId">
          <UInput v-model="state.workshopId" class="w-full" />
        </UFormField>
        <div class="flex justify-end gap-2">
          <UButton
            label="Отмена"
            color="neutral"
            variant="subtle"
            @click="open = false"
          />
          <UButton
            label="Создать"
            color="primary"
            variant="solid"
            type="submit"
          />
        </div>
      </UForm>
    </template>
  </UModal>
</template>
