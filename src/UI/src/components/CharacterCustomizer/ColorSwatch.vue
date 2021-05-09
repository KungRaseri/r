<template>
    <v-card :outlined="false" class="mb-1" :color="Colors[Index]" height="25" width="25" @click="changecolor()" />
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class ColorSwatch extends Vue {
        @Prop(Number) index = this.Index;
        @Prop(String) colors = this.Colors;

        @Emit()
        changecolor() {
            return this.Index;
        }

        get Index() {
            return this.index;
        }

        set Index(value: number) {
            this.index = value;
        }

        get Colors() {
            return this.colors;
        }

        set Colors(value: string[]) {
            this.colors = value;
        }
    }
</script>

<style scoped>
</style>