<template>
    <v-container class="outter">
        <NewOrExisting @nextmenu="NextMenu" :show="ShowNewOrExisting"/>
        <v-container v-show="ShowSubmenu()">
            <NewCharacter @goback="GoBack" @saveform="SaveForm" :show="ShowMenu()"/>
            <ExistingCharacter @goback="GoBack" :show="!ShowMenu()" />
        </v-container>
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import NewOrExisting from './NewOrExisting.vue';
    import NewCharacter from './NewCharacter.vue';
    import ExistingCharacter from './ExistingCharacter.vue';

    @Component({
        components: {
            NewOrExisting,
            NewCharacter,
            ExistingCharacter
        },
    })
    export default class CharacterSelect extends Vue {
        menu = "";
        showNewOrExisting = false;

        $axios: any;

        mounted() {
            this.ShowNewOrExisting = true;
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_CHARACTER_SELECT":
                        this.ShowNewOrExisting = e.data.visible;
                        break;
                    default:
                        break;
                }
            });
        }

        NextMenu(value: string) {
            this.Menu = value;
            this.ShowNewOrExisting = false;
        }

        GoBack(value: boolean) {
            this.ShowNewOrExisting = true;
            this.Menu = "";
        }

        SaveForm() {
            this.Menu = "";
        }

        ShowMenu(value: string) {
            if (this.Menu === value) {
                return true
            }

            return false;
        }

        ShowSubmenu() {
            if (this.Menu !== "") {
                return true;
            }

            this.$axios
                .post(
                    "http://framework/POPULATE_CHARACTER_SELECT",
                    {
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });

            return false;
        }

        get ShowNewOrExisting() {
            return this.showNewOrExisting;
        }

        set ShowNewOrExisting(value: boolean) {
            this.showNewOrExisting = value;
        }

        get Menu() {
            return this.menu;
        }

        set Menu(value: string) {
            this.menu = value;
        }
    }
</script>

<style scoped>
</style>