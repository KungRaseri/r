<template>
    <v-fade-transition>
        <v-container v-show="Show" class="panel">
            <v-card class="card" dark>
                <h1>Character Select</h1>
                <v-divider class="my-4" />
                <v-btn block @click="nextmenu('new')">New Character</v-btn>
                <v-btn block @click="nextmenu('exist')">Select Character</v-btn>
            </v-card>
        </v-container>
    </v-fade-transition>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class NewOrExisting extends Vue {
        @Prop(Boolean) show = this.Show;

        @Emit()
        nextmenu(value: string) {
            return value;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }
    }
</script>

<style scoped>
    .outter {
        height: 100%;
        margin: 0;
        padding: 0;
        max-width: 10000px;
    }

    .panel {
        width: 25%;
        height: 100%;
        margin: auto;
        padding: 0;
    }

    .card {
        position: absolute;
        width: 25%;
        top: 50%;
        transform: translateY(-50%);
    }

    h1 {
        text-align: center;
    }
</style>