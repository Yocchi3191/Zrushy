# デフォルトレシピ
default:
    @just --list

# ラベル同期ツールの更新
update-sync-labels:
    rm -rf ./tools/gh-sync-labels
    git clone https://github.com/Yocchi3191/gh-sync-labels ./tools/gh-sync-labels
    rm -rf ./tools/gh-sync-labels/.git

# ビルドしてテストする
build:
    cd core && dotnet build Zrushy.slnx
    @just test

# テスト
test:
    cd core && dotnet test
