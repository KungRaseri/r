<template>
    <v-container class="main">
        <NewOrExisting @nextmenu="NextMenu" :show="ShowNewOrExisting"/>
        <v-container v-show="ShowSubmenu()">
            <NewCharacter @goback="GoBack" @saveform="SaveForm" :show="ShowMenu()"/>
            <span class="test" v-show="!ShowMenu()">Exist</span>
        </v-container>
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import NewOrExisting from './NewOrExisting.vue';
    import NewCharacter from './NewCharacter.vue';

    @Component({
        components: {
            NewOrExisting,
            NewCharacter
        },
    })
    export default class CharacterSelect extends Vue {
        menu = "";
        showNewOrExisting = false;

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
            this.showNewOrExisting = false;
        }

        GoBack(value: string) {
            this.showNewOrExisting = true;
            this.Menu = "";
        }

        SaveForm(value: string) {
            this.Menu = "";
        }

        ShowMenu() {
            if (this.Menu === "new") {
                return true
            }

            return false;
        }

        ShowSubmenu() {
            if (this.Menu !== "") {
                return true;
            }

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