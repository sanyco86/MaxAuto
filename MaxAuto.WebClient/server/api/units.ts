import type { Unit } from "~/types/unit";

const units: Unit[] = [
  {
    id: 1,
    name: "шт"
  },
  {
    id: 2,
    name: "л"
  },
  {
    id: 3,
    name: "кг"
  }
]

export default eventHandler(async () => {
  return units
})
