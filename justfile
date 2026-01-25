# デフォルトレシピ
default:
    @just --list

# ラベル同期ツールの更新
update-sync-labels:
    rm -rf ./tools/gh-sync-labels
    git clone https://github.com/Yocchi3191/gh-sync-labels ./tools/gh-sync-labels
    rm -rf ./tools/gh-sync-labels/.git
