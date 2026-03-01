import type { Part } from "~/types/part";

const parts: Part[] = [
  {
    id: 1,
    name: "Антифриз"
  },
  {
    id: 2,
    name: "Масло моторное"
  },
  {
    id: 3,
    name: "Масло трансмиссионное"
  }
]

export default eventHandler(async () => {
  return parts
})
