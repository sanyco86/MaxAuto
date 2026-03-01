<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'


const schema = z.object({
  firstName: z.string().min(2, 'Too short'),
  lastName: z.string().min(2, 'Too short'),
  email: z.email('Invalid email'),
  password: z.string().min(6, 'Password must be at least 6 characters').optional(),
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
  toast.add({ title: 'Success', description: `New user ${event.data.email} added`, color: 'success' })
  open.value = false
}
</script>

<template>
  <UModal v-model:open="open" title="New user" description="Add a new user to the database">
    <UButton label="New user" icon="i-lucide-plus" />

    <template #body>
      <UForm
        :schema="schema"
        :state="state"
        class="space-y-4"
        @submit="onSubmit"
      >
        <UFormField label="First Name" placeholder="John" name="firstName">
          <UInput v-model="state.firstName" class="w-full" />
        </UFormField>
        <UFormField label="Last Name" placeholder="Doe" name="lastName">
          <UInput v-model="state.lastName" class="w-full" />
        </UFormField>
        <UFormField label="Email" placeholder="john.doe@example.com" name="email">
          <UInput v-model="state.email" class="w-full" />
        </UFormField>
        <UFormField label="Password" placeholder="********" name="password">
          <UInput v-model="state.password" class="w-full" />
        </UFormField>
        <UFormField label="position" placeholder="Администратор" name="position">
          <UInput v-model="state.position" class="w-full" />
        </UFormField>
        <UFormField label="role" placeholder="visitor" name="role">
          <UInput v-model="state.role" class="w-full" />
        </UFormField>
        <UFormField label="Workshop" placeholder="visitor" name="workshopId">
          <UInput v-model="state.workshopId" class="w-full" />
        </UFormField>
        <div class="flex justify-end gap-2">
          <UButton
            label="Cancel"
            color="neutral"
            variant="subtle"
            @click="open = false"
          />
          <UButton
            label="Create"
            color="primary"
            variant="solid"
            type="submit"
          />
        </div>
      </UForm>
    </template>
  </UModal>
</template>
