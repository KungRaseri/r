<template>
    <v-container class="outter">
        <PedSelect :show="ShowPedSelect" @sendped="SetPed"/>
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import PedSelect from './PedSelect.vue';

    @Component({
        components: {
            PedSelect
        },
    })
    export default class CharacterCustomizer extends Vue {
        showPedSelect = false;
        ped = "";

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_PED_SELECT":
                        this.ShowPedSelect = e.data.visible;
                        break;
                    default:
                        break;
                }
            });
        }

        SetPed(value: string) {
            this.Ped = value;
        }

        get ShowPedSelect() {
            return this.showPedSelect;
        }

        set ShowPedSelect(value: boolean) {
            this.showPedSelect = value;
        }

        get Ped() {
            return this.ped;
        }

        set Ped(value: string) {
            this.ped = value;
        }
    }
</script>

<style scoped>
</style>