<template>
    <v-container class="outter">
        <PedSelect :show="ShowPedSelect" @sendped="SetPed"/>
        <PedCustomize :show="ShowPedCustomize" @goback="GoBack" />
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import PedSelect from './PedSelect.vue';
    import PedCustomize from './PedCustomize.vue';

    @Component({
        components: {
            PedSelect,
            PedCustomize
        },
    })
    export default class CharacterCustomizer extends Vue {
        showPedSelect = false;
        showPedCustomize = false;
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
            this.ShowPedSelect = false;
            this.ShowPedCustomize = true;
        }

        GoBack(value: boolean) {
            this.ShowPedCustomize = false;
            this.ShowPedSelect = true;
        }

        get ShowPedSelect() {
            return this.showPedSelect;
        }

        set ShowPedSelect(value: boolean) {
            this.showPedSelect = value;
        }

        get ShowPedCustomize() {
            return this.showPedCustomize;
        }

        set ShowPedCustomize(value: boolean) {
            this.showPedCustomize = value;
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